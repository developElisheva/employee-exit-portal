import httpClient from "../api/httpClient";
import type { ExitFormDetails } from "../models/exitForm";

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

// New: fetch exit forms filtered for the current role (server reads role from JWT)
export async function getExitFormsForRole(): Promise<ExitFormDetails[]> {
  const response = await httpClient.get("/ExitForms/for-role");
  return response.data;
}
