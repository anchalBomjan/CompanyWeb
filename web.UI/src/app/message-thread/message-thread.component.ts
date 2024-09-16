

import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { TimeagoModule } from 'ngx-timeago';
import { Message } from '../interface/message';
import { IUser } from '../interface/User';
import { AuthService } from '../services/auth.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-message-thread',
  standalone: true,
  imports: [FormsModule, CommonModule, TimeagoModule],
  templateUrl: './message-thread.component.html',
  styleUrls: ['./message-thread.component.css'] // Correct spelling to `styleUrls`
})
export class MessageThreadComponent {

  currentUsername: string | null = null; // Store the current user's username
  recipientUsername: string | null = null; // Store the recipient's username
  messages: Message[] = []; // Store the fetched messages
  newMessage: string = ''; // Store the typed message

  constructor(
    private messageService: MessageService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private http: HttpClient
  ) {
    // Subscribe to the current user observable to get user details
    this.authService.currentUser$.subscribe((user: IUser | null) => {
      if (user) {
        this.currentUsername = user.Username;  // Store the current user's username

        // Get recipient's username from the route if available
        this.route.paramMap.subscribe(params => {
          this.recipientUsername = params.get('username') ?? '';
          
          // If both current user and recipient usernames are available, create a Hub connection
          // if (this.recipientUsername) {
          //   this.messageService.createHubConnection(user, this.recipientUsername);
          // }
        });
      }
    });

    // Load the message thread when the route changes
    this.route.params.subscribe(params => {
      this.recipientUsername = params['username'];
      this.loadMessageThread();
    });
  }

  // Load the message thread
  loadMessageThread(): void {
    if (this.recipientUsername) {
      this.messageService.getMessageThread(this.recipientUsername).subscribe({
        next: (response: Message[]) => {
          this.messages = response; // Assign the response to the messages array
        },
        error: (error) => {
          console.error('Error fetching message thread:', error);
        }
      });
    }
  }

  sendMessage(): void {
    if (this.newMessage.trim() && this.recipientUsername) {
      this.messageService
        .sendMessage(this.recipientUsername, this.newMessage)
        .subscribe({
          next: () => {
            this.newMessage = ''; // Clear the input field after sending
          },
          error: (error) => {
            console.error('Error sending message:', error);
          }
        });
    }
  }


}