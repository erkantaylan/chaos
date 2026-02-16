import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EnvironmentService } from '@abp/ng.core';
import { Observable } from 'rxjs';
import { CreateTodoDto, PagedResultDto, TodoDto } from './todo.models';

@Injectable({ providedIn: 'root' })
export class TodoService {
  private http = inject(HttpClient);
  private environment = inject(EnvironmentService);

  private get apiUrl(): string {
    return this.environment.getApiUrl('default') + '/api/app/todo';
  }

  getList(skipCount = 0, maxResultCount = 10): Observable<PagedResultDto<TodoDto>> {
    return this.http.get<PagedResultDto<TodoDto>>(this.apiUrl, {
      params: {
        SkipCount: skipCount.toString(),
        MaxResultCount: maxResultCount.toString()
      }
    });
  }

  create(input: CreateTodoDto): Observable<TodoDto> {
    return this.http.post<TodoDto>(this.apiUrl, input);
  }

  complete(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/complete`, {});
  }
}
