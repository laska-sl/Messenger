import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

import { Message } from './clients/models/message';
import { ChatService } from './services/chat.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: []
})
export class AppComponent implements OnInit {
  message: Message = {
    text: '',
    number: 0
  };
  messages: Message[] = [];
  hubConnection: HubConnection;
  foundMessages: Message[];
  dateFrom: Date;
  dateTo: Date;

  constructor(private chatService: ChatService) { }

  ngOnInit() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('NewMessage')
      .build();

    this.hubConnection
      .start()
      .catch(error => console.error('Error while starting connection: ' + error));

    this.hubConnection.on('NewMessage', data => this.messages.push(data));
  }

  sendMessage() {
    this.hubConnection.invoke('NewMessage', this.message);
    this.message.text = '';
    this.message.number++;
  }

  showMessages() {
    this.chatService
      .getMessagesInInterval({ from: this.dateFrom, to: this.dateTo })
      .subscribe(data => this.foundMessages = data);
  }
}
