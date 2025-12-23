import { useEffect, useState } from "react";
import {
    getExitFormDetails
} from "./hrExitFormsService";
import type { ExitFormDetails } from "../models/exitForm";

interface Props {
    formId: number;
    onBack: () => void;
}

export default function HrExitFormDetailsPage({ formId, onBack }: Props) {
    const [form, setForm] = useState<ExitFormDetails | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        getExitFormDetails(formId)
            .then(setForm)
            .finally(() => setLoading(false));
    }, [formId]);

    if (loading) return <p>טוען טופס…</p>;
    if (!form) return <p>טופס לא נמצא</p>;

    return (
        <div style={{ padding: 24 }}>
            <button onClick={onBack}>⬅ חזרה לרשימה</button>

            <h1 style={{ textAlign: "center" }}>טופס טיולים</h1>

            <section style={{ marginBottom: 24 }}>
                <p><strong>שם העובד:</strong> {form.employeeName}</p>
                <p><strong>ת״ז:</strong> {form.employeeTz}</p>
                <p><strong>תאריך עזיבה:</strong> {new Date(form.endDate).toLocaleDateString()}</p>
            </section>

            <table width="100%" border={1} cellPadding={8}>
                <thead>
                    <tr>
                        <th>תחום</th>
                        <th>נושא</th>
                        <th>סטטוס</th>
                        <th>אושר ע״י</th>
                        <th>תאריך</th>
                        <th>הערות</th>
                    </tr>
                </thead>
                <tbody>
                    {form.tasks.map(t => (
                        <tr key={t.id}>
                            <td>{t.department}</td>
                            <td>{t.topic}</td>
                            <td>{t.isApproved ? "✔" : "✖"}</td>
                            <td>{t.approvedBy || ""}</td>
                            <td>{t.approvedAt ? new Date(t.approvedAt).toLocaleDateString() : ""}</td>
                            <td>{t.comment || ""}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
