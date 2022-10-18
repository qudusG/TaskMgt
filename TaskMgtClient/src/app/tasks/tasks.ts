export interface ITask {
    id: number;
    description: string;
    dateCreated: Date;
    requiredDate: Date;
    nextActionDate: Date;
    title: string;
    status: number;
    type: number;
    createdBy: string;
    assignedTo: string;
  }