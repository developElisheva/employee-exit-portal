import httpClient from "../api/httpClient";
import type { ExitFormDetails } from "../models/exitForm";
import type { HrExitFormListItem } from "../models/hrExitForms";

export const getHrExitForms = async (): Promise<HrExitFormListItem[]> => {
    const res = await httpClient.get("/ExitForms/hr");
    return res.data;
};

export const getExitFormDetails = async (
    id: number
): Promise<ExitFormDetails> => {
    const res = await httpClient.get(`/ExitForms/${id}`);
    const data: any = res.data;

    // Some APIs return `exitDate` while our model expects `endDate`.
    // Normalize the property to `endDate` so the UI can rely on a single field.
    if (data.exitDate && !data.endDate) {
        data.endDate = data.exitDate;
    }

    // Map common ID/TZ field names to `employeeTz` so UI can display it even if server uses a different key
    if (!data.employeeTz) {
        data.employeeTz = data.tz ?? data.idNumber ?? data.id ?? data.employeeId ?? data.identityNumber ?? data.identity;
    }

    // Debug: log raw response so developers can inspect when tasks are empty.
    // Remove or guard this in production if privacy concerns exist.
    try { console.debug("getExitFormDetails response:", data); } catch (e) { /* ignore */ }

    return data as ExitFormDetails;
};
