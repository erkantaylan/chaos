import { Injectable, inject, OnDestroy } from '@angular/core';
import { EnvironmentService, AuthService } from '@abp/ng.core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { TodoDto } from './todo.models';

@Injectable({ providedIn: 'root' })
export class TodoHubService implements OnDestroy {
  private environment = inject(EnvironmentService);
  private authService = inject(AuthService);
  private hubConnection?: signalR.HubConnection;

  private _todoCreated = new Subject<TodoDto>();
  private _todoCompleted = new Subject<TodoDto>();

  todoCreated$ = this._todoCreated.asObservable();
  todoCompleted$ = this._todoCompleted.asObservable();

  async start(): Promise<void> {
    if (this.hubConnection) {
      return;
    }

    const apiUrl = this.environment.getApiUrl('default');
    const token = await this.authService.getAccessToken();

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${apiUrl}/signalr-hubs/todo`, {
        accessTokenFactory: () => token || ''
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.on('TodoCreated', (todo: TodoDto) => {
      this._todoCreated.next(todo);
    });

    this.hubConnection.on('TodoCompleted', (todo: TodoDto) => {
      this._todoCompleted.next(todo);
    });

    try {
      await this.hubConnection.start();
    } catch (err) {
      console.error('Error starting TodoHub connection:', err);
    }
  }

  async stop(): Promise<void> {
    if (this.hubConnection) {
      await this.hubConnection.stop();
      this.hubConnection = undefined;
    }
  }

  ngOnDestroy(): void {
    this.stop();
  }
}
