import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { TaskService } from 'src/app/tasks/tasks.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  sub!: Subscription;
  errorMessage = '';
  stats: any = {};
  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.sub = this.taskService.getStats().subscribe({
      next: (stats: any) => {
        this.stats = stats;
      },
      error: (err: string) => this.errorMessage = err
    });
  }

}
