
// import { HttpClient } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { BehaviorSubject, Observable, take } from 'rxjs';
// import { environment } from '../environment/environment';
// import { Message } from '../interface/message';
// import { getPaginatedResult, getPaginationHeaders } from './pagination-helper.service';

// import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
// import { IUser } from '../interface/User';
// import { Group } from '../interface/group';

// @Injectable({
//   providedIn: 'root'
// })
// export class MessageService {
//   baseUrl = environment.apiUrl;
//   hubUrl = environment.hubUrl;
//     private hubConnection: HubConnection;
//   private messageThreadSource = new BehaviorSubject<Message[]>([]);
//   messageThread$ = this.messageThreadSource.asObservable();

 

//   constructor(private http: HttpClient) {}

  
//   createHubConnection(user: IUser, otherUsername: string) {
 
  
//     const startConnection = () => {
//       this.hubConnection = new HubConnectionBuilder()
//         .withUrl(this.hubUrl + 'message?user=' + otherUsername, {
//           accessTokenFactory: () => user.Token
//         })
//         .withAutomaticReconnect()
//         .configureLogging(LogLevel.Information)
//         .build();
  

//         this.hubConnection.on('NewMessage', (message: Message) => {
//           this.messageThread$.pipe(take(1)).subscribe(messages => {
//             const updatedMessages = [...messages, message];
//             console.log('Updated messages:', updatedMessages);
//             this.messageThreadSource.next(updatedMessages);
//           });
//         });

//               // Listen to new messages
//       this.hubConnection.on('ReceiveMessageThread', (messages: Message[]) => {
//         console.log('Received message thread:', messages);
//         this.messageThreadSource.next(messages);
//       });

//         this.hubConnection.on('UpdatedGroup', (group:Group)=>{
//                 if(group.connections.some(x=>x.username===otherUsername)){
//                   this.messageThread$.pipe(take(1)).subscribe(messages=>{
//                     messages.forEach(message=>{
//                       if(!message.dateRead){
//                         message.dateRead= new Date(Date.now())
//                       }
//                     })
//                     this.messageThreadSource.next([...messages]);
//                   })
//                 }
//               })
  
//       // Handle connection lifecycle events
//       this.hubConnection.onreconnecting((error) => {
//         console.log('Hub connection is reconnecting...', error);
//       });
  
//       this.hubConnection.onreconnected((connectionId) => {
//         console.log('Hub connection reconnected:', connectionId);
//       });
  
//       this.hubConnection.onclose((error) => {
//         console.error('Hub connection closed:', error);
//         // Optionally, retry connection or handle disconnection state
//       });
  
//       this.hubConnection.start()
//         .then(() => {
//           console.log("Hub connection started");

//         })
//         .catch(error => {
//           console.error("Hub connection error:", error);
         
          
//         });
  

//     };
  
//     startConnection(); // Initiate the connection
//   }

//   // Disconnect from the hub
//   stopHubConnection() {
//     if (this.hubConnection) {
//       this.hubConnection.stop().catch(error => console.error(error));
//     }
//   }

//   // Get paginated messages
//   getMessages(pageNumber: number, pageSize: number, container: string) {
//     let params = getPaginationHeaders(pageNumber, pageSize);
//     params = params.append('Container', container);
//     return getPaginatedResult<Message[]>(this.baseUrl + 'message', params, this.http);
//   }

//   // Get the message thread with a specific user
//   getMessageThread(username: string): Observable<Message[]> {
//     return this.http.get<Message[]>(this.baseUrl + 'message/thread/' + username);
//   }

//   // Send a message to a user
//   sendMessage(recipientUsername: string, content: string): Observable<Message[]> {
//     const message = {
//       recipientUsername,
//       content
//     };
//     return this.http.post<Message[]>(this.baseUrl + 'message', message);
//   }

//   // Delete a message
//   deleteMessage(id: number): Observable<any> {
//     return this.http.delete(`${this.baseUrl}message/${id}`);
//   }
// }
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, take } from 'rxjs';
import { environment } from '../environment/environment';
import { Message } from '../interface/message';
import { getPaginatedResult, getPaginationHeaders } from './pagination-helper.service';

import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { IUser } from '../interface/User';
import { Group } from '../interface/group';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
    private hubConnection: HubConnection;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

 

  constructor(private http: HttpClient) {}

  
  createHubConnection(user: IUser, otherUsername: string) {
 
  
    const startConnection = () => {
      this.hubConnection = new HubConnectionBuilder()
        .withUrl(this.hubUrl + 'message?user=' + otherUsername, {
          accessTokenFactory: () => user.Token
        })
        .withAutomaticReconnect()
        .configureLogging(LogLevel.Information)
        .build();
  

        this.hubConnection.on('NewMessage', (message: Message) => {
          this.messageThread$.pipe(take(1)).subscribe(messages => {
            const updatedMessages = [...messages, message];
            console.log('Updated messages:', updatedMessages);
            this.messageThreadSource.next(updatedMessages);
          });
        });

              // Listen to new messages
      this.hubConnection.on('ReceiveMessageThread', (messages: Message[]) => {
        console.log('Received message thread:', messages);
        this.messageThreadSource.next(messages);
      });

        this.hubConnection.on('UpdatedGroup', (group:Group)=>{
                if(group.connections.some(x=>x.username===otherUsername)){
                  this.messageThread$.pipe(take(1)).subscribe(messages=>{
                    messages.forEach(message=>{
                      if(!message.dateRead){
                        message.dateRead= new Date(Date.now())
                      }
                    })
                    this.messageThreadSource.next([...messages]);
                  })
                }
              })
  
      // Handle connection lifecycle events
      this.hubConnection.onreconnecting((error) => {
        console.log('Hub connection is reconnecting...', error);
      });
  
      this.hubConnection.onreconnected((connectionId) => {
        console.log('Hub connection reconnected:', connectionId);
      });
  
      this.hubConnection.onclose((error) => {
        console.error('Hub connection closed:', error);
        // Optionally, retry connection or handle disconnection state
      });
  
      this.hubConnection.start()
        .then(() => {
          console.log("Hub connection started");

        })
        .catch(error => {
          console.error("Hub connection error:", error);
         
          
        });
  

    };
  
    startConnection(); // Initiate the connection
  }

  // Disconnect from the hub
  stopHubConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop().catch(error => console.error(error));
    }
  }

  // Get paginated messages
  getMessages(pageNumber: number, pageSize: number, container: string) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);
    return getPaginatedResult<Message[]>(this.baseUrl + 'message', params, this.http);
  }

  // Get the message thread with a specific user
  getMessageThread(username: string): Observable<Message[]> {
    return this.http.get<Message[]>(this.baseUrl + 'message/thread/' + username);
  }

  // Send a message to a user
  sendMessage(recipientUsername: string, content: string): Observable<Message[]> {
    const message = {
      recipientUsername,
      content
    };
    return this.http.post<Message[]>(this.baseUrl + 'message', message);
  }

  // Delete a message
  deleteMessage(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}message/${id}`);
  }
}