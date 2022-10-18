import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { TaskService } from '../tasks/tasks.service';
import Swal from 'sweetalert2';
import { TokenStorageService } from '../auth-layout/token-storage.service';
@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.scss']
})
export class TaskDetailComponent implements OnInit {
  classes = ['comment mt-4 text-justify float-left', 
    'text-justify darker mt-4 float-right', 
    'comment mt-4 text-justify', 'darker mt-4 text-justify'];
  index: number = 1;
  task: any = {
    title: '',
    description:''
  };
  searchText = '';
  public focus: any;
  sub!: Subscription;
  errorMessage = '';
  statusBadge = '';
  statusName = '';
  isTaskOwner = false;
  commentFormGroup: FormGroup = this.fb.group({
    text: ["", Validators.required],
    type: ["", Validators.required],
    reminderDate: ["", Validators.required],
  });

  statusFormGroup: FormGroup = this.fb.group({
    id: ["", Validators.required],
    status: ["", Validators.required]
  });
 
  constructor(private taskService: TaskService, 
    private route: ActivatedRoute, 
    private fb: FormBuilder,
    private tokenStorage: TokenStorageService) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.sub = this.taskService.getTask(id).subscribe({
      next: (task: any) => {
        this.task = task;
        this.statusBadge = this.getStatusBadge();
        this.statusName = this.getStatusName();
        this.isTaskOwner = this.tokenStorage.getUser().id == task.createdById;
      },
      error: (err: string) => this.errorMessage = err
    });
  }

  AddComment(){
    let formValue = this.commentFormGroup.value;
    const params: any = {
      taskId: this.task.id,
      text: formValue.text,
      type: +formValue.type,
      reminderDate: formValue.reminderDate,
    }
    this.taskService.addComment(params).subscribe({
      next: data => {
        Swal.fire({
          title: 'Comment added',
          text: '',
          icon: 'success'
        })
        window.location.reload();
      },
      error: err => {
        this.errorMessage = err.error.message;
      }
    })
  }
  ChangeStatus(){
    let taskd = {
      id: this.task.id,
      status: +this.statusFormGroup.value.status
    }
    this.taskService.changeStatus(taskd).subscribe({
    })
    window.location.reload();
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  getClass(): number{
    if (this.index == 0)
      this.index = 1;

    else
      this.index = 0;
    
    return this.index;
  }
  getStatusBadge(): string{
    if (this.task.status == 0)
      return "badge-primary";
    if (this.task.status == 1)
      return "badge-info";
    return "badge-success";
  }
  getStatusName(): string{
    if (this.task.status == 0)
      return "New";
    if (this.task.status == 1)
      return "In Progress";
    return "Completed";
  }
}
