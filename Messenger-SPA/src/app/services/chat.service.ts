import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { DateInterval } from '../clients/models/date-interval';
import { Message } from '../clients/models/message';

@Injectable()
export class ChatService {
  messages: Message[];

  constructor(private http: HttpClient) { }

  getMessagesInInterval(dateInterval: DateInterval): Observable<Message[]> {
    const parameters = new HttpParams()
      .append('from', dateInterval.from.toString())
      .append('to', dateInterval.to.toString());

    return this.http.get<Message[]>(environment.apiUrl + 'messenger', { params: parameters });
  }
}
