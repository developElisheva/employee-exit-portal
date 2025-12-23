import { useEffect, useState } from "react";
import {
    getHrExitForms
} from "../services/hrExitFormsService";
import type { HrExitFormListItem } from "../models/hrExitForms";

interface Props {
    onSelectForm: (formId: number) => void;
}

export default function HrExitFormsListPage({ onSelectForm }: Props) {
    const [forms, setForms] = useState<HrExitFormListItem[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        getHrExitForms()
            .then(setForms)
            .finally(() => setLoading(false));
    }, []);

    if (loading) return <p>טוען טפסים…</p>;

    return (
        <div style={{ padding: 24 }}>
            <h2>ניהול טפסי עזיבה</h2>

            <table width="100%" border={1} cellPadding={8}>
                <thead>
                    <tr>
                        <th>שם עובד</th>
                        <th>ת״ז</th>
                        <th>תאריך עזיבה</th>
                        <th>סטטוס</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {forms.map(f => (
                        <tr key={f.id}>
                            <td>{f.employeeName}</td>
                            <td>{f.employeeTz}</td>
                            <td>{new Date(f.endDate).toLocaleDateString()}</td>
                            <td>{f.isCompleted ? "✔ הושלם" : "⏳ פתוח"}</td>
                            <td>
                                <button onClick={() => onSelectForm(f.id)}>
                                    פתח טופס
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
