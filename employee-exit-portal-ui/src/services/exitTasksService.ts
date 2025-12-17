import httpClient from "../api/httpClient";
import type { ExitTasksGroup } from "../models/exitTasks";

/* =======================
   משימות לפי Role
======================= */
export async function getGroupedTasksByRole(
  role: string
): Promise<ExitTasksGroup[]> {
  const response = await httpClient.get(
    `/exittasks/grouped?role=${role}`
  );
  return response.data;
}

/* =======================
   אישור משימה (חתימה)
======================= */
export async function approveTask(
  taskId: number,
  role: string,
  comment: string
): Promise<void> {
  await httpClient.post(
    `/exittasks/${taskId}/approve?role=${role}`,
    comment
  );
}

/* =======================
   🔹 שליפת כל התחומים (Roles)
======================= */
export async function getRoles(): Promise<string[]> {
  const response = await httpClient.get("/exittasks/roles");
  return response.data;
}
