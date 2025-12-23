import { useEffect, useState } from "react";
import { getHrExitForms  } from "../services/hrExitFormsService";
import type { HrExitFormListItem } from "../models/hrExitForms";

interface Props {
  onSelectForm: (id: number) => void;
}

export default function HrExitFormsListPage({ onSelectForm }: Props) {
  const [forms, setForms] = useState<HrExitFormListItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let mounted = true;
    setLoading(true);
    setError(null);

    (async () => {
      try {
        const res = await getHrExitForms();
        if (!mounted) return;
        setForms(res);
      } catch (err: any) {
        console.error("Failed to load HR exit forms:", err);
        if (!mounted) return;
        setError(err?.message || "שגיאת רשת");
      } finally {
        if (mounted) setLoading(false);
      }
    })();

    return () => { mounted = false; };
  }, []);

  if (loading) return <p>טוען טפסים…</p>;

  return (
    <div style={{ padding: 24 }}>
      <h1>טפסי עזיבה</h1>

      {error && (
        <div style={{ color: "#b91c1c", marginBottom: 12 }}>שגיאה בטעינת טפסים: {error}</div>
      )}

      {forms.length === 0 && !error ? (
        <p>לא נמצאו טפסים להצגה.</p>
      ) : (
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
      )}
    </div>
  );
}
