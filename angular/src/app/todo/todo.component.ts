import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LocalizationPipe } from '@abp/ng.core';
import { Subscription } from 'rxjs';
import { TodoService } from './todo.service';
import { TodoHubService } from './todo-hub.service';
import { TodoDto, CreateTodoDto } from './todo.models';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.scss'],
  imports: [CommonModule, FormsModule, LocalizationPipe]
})
export class TodoComponent implements OnInit, OnDestroy {
  private todoService = inject(TodoService);
  private todoHubService = inject(TodoHubService);
  private subscriptions: Subscription[] = [];

  todos: TodoDto[] = [];
  loading = false;
  creating = false;
  completing: Record<string, boolean> = {};
  isCreateModalOpen = false;
  newTodo: CreateTodoDto = { title: '', description: '' };

  ngOnInit(): void {
    this.loadTodos();
    this.setupSignalR();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
    this.todoHubService.stop();
  }

  private async setupSignalR(): Promise<void> {
    await this.todoHubService.start();

    this.subscriptions.push(
      this.todoHubService.todoCreated$.subscribe(todo => {
        if (!this.todos.find(t => t.id === todo.id)) {
          this.todos = [todo, ...this.todos];
        }
      }),
      this.todoHubService.todoCompleted$.subscribe(completedTodo => {
        const index = this.todos.findIndex(t => t.id === completedTodo.id);
        if (index !== -1) {
          this.todos[index] = completedTodo;
          this.todos = [...this.todos];
        }
      })
    );
  }

  loadTodos(): void {
    this.loading = true;
    this.todoService.getList(0, 100).subscribe({
      next: result => {
        this.todos = result.items;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  openCreateModal(): void {
    this.newTodo = { title: '', description: '' };
    this.isCreateModalOpen = true;
  }

  closeCreateModal(): void {
    this.isCreateModalOpen = false;
  }

  createTodo(): void {
    if (!this.newTodo.title) return;

    this.creating = true;
    this.todoService.create(this.newTodo).subscribe({
      next: todo => {
        if (!this.todos.find(t => t.id === todo.id)) {
          this.todos = [todo, ...this.todos];
        }
        this.closeCreateModal();
        this.creating = false;
      },
      error: () => {
        this.creating = false;
      }
    });
  }

  completeTodo(todo: TodoDto): void {
    this.completing[todo.id] = true;
    this.todoService.complete(todo.id).subscribe({
      next: () => {
        todo.isCompleted = true;
        this.completing[todo.id] = false;
      },
      error: () => {
        this.completing[todo.id] = false;
      }
    });
  }
}
