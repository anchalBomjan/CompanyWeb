import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { TimeagoModule } from 'ngx-timeago';
import { take } from 'rxjs';
import { Member } from '../interface/member';
import { Message } from '../interface/message';
import { Pagination } from '../interface/pagination';
import { IUser } from '../interface/User';
import { AdminService } from '../services/admin.service';
import { AuthService } from '../services/auth.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, TimeagoModule, NgxPaginationModule],
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']  // Changed to `styleUrls` (correct spelling)
})
export class MessageComponent {
  messages: Message[] = [];
  pagination!: Pagination;
  container = 'Unread';
  pageNumber = 1;
  pageSize = 5;
  loading = false;
  user!: IUser;
  usersWithRoles: any[] = [];

  constructor(
    private messageService: MessageService,
    private router: Router,
    private route: ActivatedRoute,
    private adminService: AdminService,
    private authService: AuthService
  ) {
    this.authService.currentUser$.pipe(take(1)).subscribe(user => this.user = user!);
  }

  loadMessages(): void {
    this.loading = true;
    this.messageService.getMessages(this.pageNumber, this.pageSize, this.container).subscribe({
      next: (response) => {
        this.messages = response.result;
        this.pagination = response.pagination;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading messages:', err);
        this.loading = false;
      }
    });
  }

  loadUserWithRole(): void {
    this.loading = true;
    this.adminService.getAllUsersWithRoles().subscribe({
      next: (response) => {
        this.usersWithRoles = response;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading users with roles:', err);
        this.loading = false;
      }
    });
  }

  deleteMessage(id: number): void {
    this.messageService.deleteMessage(id).subscribe(() => {
      this.messages = this.messages.filter(m => m.id !== id);
    });
  }

  pageChanged(event: any): void {
    this.pageNumber = event.page;
    this.loadMessages();
  }

  navigateToMessageThread(username: string): void {
    this.messageService.createHubConnection(this.user, username);
    const userRole = this.authService.getUserRole();
    if (userRole === 'Hr') {
      this.router.navigate(['app-hr/app-message-thread', username]);
    } else if (userRole === 'Admin') {
      this.router.navigate(['app-admin/app-message-thread', username]);
    } else {
      console.error('Access denied: You do not have permission to view this thread.');
      this.router.navigate(['/unauthorized']);
    }
  }
}




