import { useEffect, useState } from "react";
import { getExitFormDetails } from "../services/hrExitFormsService";
import type { ExitFormDetails } from "../models/exitForm";

interface Props { formId: number; onBack: () => void }

export default function HrExitFormDetailsPage({ formId, onBack }: Props) {
  const [details, setDetails] = useState<ExitFormDetails | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setLoading(true);
    getExitFormDetails(formId).then(setDetails).finally(() => setLoading(false));
  }, [formId]);

  if (loading) return <p>טוען טופס…</p>;
  if (!details) return (
    <div style={{ padding: 24, direction: 'rtl' }}>
      <button onClick={onBack} style={{ marginBottom: 12 }}>חזור</button>
      <p>טופס לא נמצא.</p>
    </div>
  );

  const tasks: any[] = Array.isArray(details.tasks) ? details.tasks : [];

  const fmt = (v: any) => {
    if (!v && v !== 0) return "-";
    if (typeof v === 'string' && /^\d{4}-\d{2}-\d{2}(T|$)/.test(v)) {
      const d = new Date(v); if (!isNaN(d.getTime())) return d.toLocaleDateString('he-IL');
    }
    return String(v);
  };

  return (
    <div style={{ padding: 24, direction: 'rtl' }}>
      <button onClick={onBack} style={{ marginBottom: 12 }}>חזור</button>

      <div className="details-card">
        <h2 className="card-name">{details.employeeName ? `טופס: ${details.employeeName}` : 'פרטי טופס'}</h2>

        <div className="details-grid">
          <div>
            <div className="details-label">שם:</div>
            <div className="details-value">{fmt(details.employeeName)}</div>
          </div>
          <div>
            <div className="details-label">ת"ז:</div>
            <div className="details-value">{fmt((details as any).employeeTz ?? (details as any).tz ?? (details as any).id)}</div>
          </div>
          <div>
            <div className="details-label">תאריך עזיבה:</div>
            <div className="details-value">{fmt(details.endDate ?? (details as any).exitDate)}</div>
          </div>
        </div>

        {details.status && String(details.status).toLowerCase() !== 'completed' ? (
          <div style={{ color: '#b45309', fontWeight: 700, marginTop: 12 }}>סטטוס טופס: {details.status} — טופס לא הושלם</div>
        ) : null}

        <hr style={{ margin: '16px 0' }} />

        <h3 className="details-section-title">רשימת משימות</h3>

        {tasks.length === 0 ? (
          <p>אין משימות בטופס.</p>
        ) : (
          <div style={{ overflowX: 'auto' }}>
            <table className="details-table">
              <thead>
                <tr>
                  <th>מחלקה / תפקיד אחראי</th>
                  <th>הערות</th>
                  <th>אושר על ידי</th>
                  <th>תאריך אישור</th>
                  <th>סטטוס</th>
                </tr>
              </thead>
              <tbody>
                {tasks.map((t, i) => {
                  const dept = (t as any).department ?? (t as any).unit ?? (t as any).dept ?? '-';
                  const role = (t as any).responsibleRole ?? (t as any).role ?? '';
                  const signer = (t as any).approvedBy ?? (t as any).signedBy ?? '-';
                  const at = (t as any).approvedAt ?? (t as any).signedAt ?? '-';
                  const signed = Boolean(signer && signer !== '-' || at && at !== '-');
                  return (
                    <tr key={t.id ?? i}>
                      <td>{fmt([dept, role].filter(Boolean).join(' / '))}</td>
                      <td>{fmt((t as any).comments ?? (t as any).comment ?? '-')}</td>
                      <td>{fmt(signer)}</td>
                      <td>{fmt(at)}</td>
                      <td>{signed ? 'הוחתם' : 'לא הוחתם'}</td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        )}

      </div>
    </div>
  );
}
