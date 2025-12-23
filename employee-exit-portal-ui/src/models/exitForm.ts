import type { ReactNode } from "react";

export interface ExitTaskDetails {
  id: number;
  department: string;
  topic: string;
  isApproved: boolean;
  approvedBy?: string;
  approvedAt?: string;
  comment?: string;
}

export interface ExitFormDetails {
  status: ReactNode;
  id: number;
  employeeName: string;
  employeeTz: string;
  endDate: string;
  tasks: ExitTaskDetails[];
}
