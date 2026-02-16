export interface TodoDto {
  id: string;
  title: string;
  description?: string;
  isCompleted: boolean;
  completedByUserId?: string;
  completedByUserName?: string;
  completedTime?: Date;
  creatorUserName?: string;
  creationTime: Date;
}

export interface CreateTodoDto {
  title: string;
  description?: string;
}

export interface PagedResultDto<T> {
  totalCount: number;
  items: T[];
}
