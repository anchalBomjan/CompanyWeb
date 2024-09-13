import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { TimeagoModule } from 'ngx-timeago';
import { Message } from '../interface/message';
import { Pagination } from '../interface/pagination';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule,TimeagoModule, NgxPaginationModule],
  templateUrl: './message.component.html',
  styleUrl: './message.component.css'
})
export class MessageComponent {

  messages: Message[];
  pagination: Pagination
  container= 'Unread';
  pageNumber=1;
  pageSize=5;
  loading=false;
  
  constructor(private messageService: MessageService,private router:Router){
    this.loadMessages();
  }
  
  loadMessages(){
    this.loading=true;
    this.messageService.getMessages(this.pageNumber, this.pageSize, this.container).subscribe((response)=>{
      console.log(response);
      this.messages = response.result;
      this.pagination= response.pagination;
      this.loading=false;
      console.log(this.messages,'check');
      
    })
  }
  
  deleteMessage(id:number){
   
  
        this.messageService.deleteMessage(id).subscribe(()=>{
          this.messages.splice(this.messages.findIndex(m=>m.id===id),1);
        })
   
  }
  
  pageChanged(event: any){
    this.pageNumber= event.page;
    this.loadMessages();
  }

  navigateToMessageThread(username: string): void {
  



    //focus for this route
     this.router.navigate(['app-hr/app-message-thread', username]);
    console.log('Navigating to message thread for:', username);
  }
  

}
