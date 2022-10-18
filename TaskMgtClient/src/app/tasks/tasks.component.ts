import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { TokenStorageService } from '../auth-layout/token-storage.service';
import { ITask } from './tasks';
import { TaskService } from './tasks.service';
@Component({
  selector: 'pm-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent implements OnInit {

  tasks: any[] = [];
  sub!: Subscription;
  errorMessage = '';
  searchText = '';
  public focus: any;
  constructor(private taskService: TaskService, private tokenStorage: TokenStorageService) {}

  ngOnInit(): void {
    this.sub = this.taskService.getTasks().subscribe({
      next: (tasks: any[]) => {
        this.tasks = tasks;
        console.log(tasks);
        console.log(this.tokenStorage.getToken());
      },
      error: (err: string) => this.errorMessage = err
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
