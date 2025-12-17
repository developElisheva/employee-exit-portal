export interface ExitTask {
  taskId: number;
  title: string;
  responsibleRole: string;
  status: string;
  comments?: string;
  approvedAt?: string | null;
}

export interface ExitTasksGroup {
  exitFormId: number;
  employeeName: string;
  exitDate: string;
  tasks: ExitTask[];
}
