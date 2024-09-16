
// import { HttpClient } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import {   HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
// import { take } from 'rxjs';
// import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
// import { environment } from '../environment/environment';
// import { Group } from '../interface/group';
// import { Message } from '../interface/message';
// import { IUser } from '../interface/User';
// import { getPaginatedResult, getPaginationHeaders } from './pagination-helper.service';

// @Injectable({
//   providedIn: 'root'
// })
// export class MessageService {
//   baseUrl = environment.apiUrl;
//   hubUrl = environment.hubUrl;
//   private hubConnection: HubConnection;
//   private messageThreadSource = new BehaviorSubject<Message[]>([]);
//   messageThread$ = this.messageThreadSource.asObservable();
//   constructor(private http:HttpClient) { }
//   createHubConnection(user: IUser, otherUsername: string) {
//     this.hubConnection = new HubConnectionBuilder()
//       .withUrl(this.hubUrl + 'message?user=' + otherUsername, {
//         accessTokenFactory: () => user.Token
//       })
//       // .withAutomaticReconnect()
//       .configureLogging(LogLevel.Information)
//       .build();

//     // Set up listeners before starting the connection
//     this.hubConnection.on('NewMessage', message => {
//       console.log('New message received:', message);
//       this.messageThread$.pipe(take(1)).subscribe(messages => {
//         const updatedMessages = [...messages, message];
//         console.log('Updated messages:', updatedMessages);
//         this.messageThreadSource.next(updatedMessages);
//       });
//     });

//     this.hubConnection.on('ReceiveMessageThread', messages => {
//       console.log('Received message thread:', messages);
//       this.messageThreadSource.next(messages);
//     });
//     this.hubConnection.on('UpdatedGroup', (group:Group)=>{
//       if(group.connections.some(x=>x.username===otherUsername)){
//         this.messageThread$.pipe(take(1)).subscribe(messages=>{
//           messages.forEach(message=>{
//             if(!message.dateRead){
//               message.dateRead= new Date(Date.now())
//             }
//           })
//           this.messageThreadSource.next([...messages]);
//         })
//       }
//     })

//     this.hubConnection.onreconnecting(error => {
//       console.warn('Hub connection lost, attempting to reconnect...', error);
//       // Update UI to indicate reconnection in progress
//     });

//     this.hubConnection.onreconnected(connectionId => {
//       console.log('Hub connection reestablished:', connectionId);
//       // Optionally, refresh the message thread or notify the user
//     });

//     this.hubConnection.onclose(error => {
//       console.error('Hub connection closed:', error);
//       // Optionally: retry logic or user notification
//     });

//     // Start the connection after all listeners are set
//     this.hubConnection.start()
//       .then(() => console.log('Hub Connection started'))
//       .catch(error => console.log(error));
//       console.log('Hub connection state:', this.hubConnection.state);
//   }

//   stopHubConnection() {
//     if (this.hubConnection) {
//       this.hubConnection.stop();
//     }
//   }

//   getMessages(pageNumber:number, pageSize:number, container:string) {
//     let params = getPaginationHeaders(pageNumber, pageSize);
//     params = params.append('Container', container);
//     return getPaginatedResult<Message[]>(this.baseUrl + 'message', params, this.http);
//   }

//   getMessageThread(username: string) {
//     return this.http.get<Message[]>(this.baseUrl + 'message/thread/' + username);
//   }
  
 
 
  

//   async sendMessage(username: string, content: string) {
//     try {
//       if (this.hubConnection && this.hubConnection.state === 'Connected') {
//         await this.hubConnection.invoke('SendMessage', { recipientUsername: username, content });
//       } else {
//         console.log('Cannot send message, hub connection is not active.');
//       }
//     } catch (error) {
//       console.error('Error sending message:', error);
//       // Optionally, implement retry logic here
//     }
//   }

//   deleteMessage(id: number) {
//     return this.http.delete(this.baseUrl + 'messages/' + id);
//   }


// }







import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../environment/environment';
import { Message } from '../interface/message';
import { getPaginatedResult, getPaginationHeaders } from './pagination-helper.service';


@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

  constructor(private http: HttpClient) {}

    getMessages(pageNumber:number, pageSize:number, container:string) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);
    return getPaginatedResult<Message[]>(this.baseUrl + 'message', params, this.http);
  }
    getMessageThread(username: string) {
    return this.http.get<Message[]>(this.baseUrl + 'message/thread/' + username);
  }
  

  sendMessage(username: string, content: string): Observable<Message[]> {
    const message = {
      recipientUsername: username,
      content
    };
    return this.http.post<Message[]>(this.baseUrl +'message' , message);
  }

  deleteMessage(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}message/${id}`);
  }
}