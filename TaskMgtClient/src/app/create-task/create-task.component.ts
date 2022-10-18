import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { TaskService } from '../tasks/tasks.service';
import { IUser } from './user';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.scss']
})
export class CreateTaskComponent implements OnInit {

  CreateTask: FormGroup = this.fb.group({
    title: ["", Validators.required],
    description: ["", Validators.required],
    date: ["", Validators.required],
    type: ["", Validators.required],
    assignee: ["", Validators.required],
  });
 
  users: IUser[] = [];
  sub!: Subscription;
  errorMessage = '';
  constructor(
    private taskService: TaskService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.sub = this.taskService.getUsers().subscribe({
      next: (users: IUser[]) => {
        this.users = users;
      },
      error: (err: string) => this.errorMessage = err
    });
  }

  submitForm(){
    console.log(this.CreateTask.value);
    let formValue = this.CreateTask.value;
    const params: any = {
      requiredDate: formValue.date,
      type: +formValue.type,
      description: formValue.description,
      assignedToId: formValue.assignee,
      title: formValue.title
    }
    this.taskService.createTask(params).subscribe({
      next: data => {
        Swal.fire({
          title: 'Task created!',
          text: '',
          icon: 'success'
        })
        this.router.navigate(['/tasks']);
      },
      error: err => {
        this.errorMessage = err.error.message;
      }
    })
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
