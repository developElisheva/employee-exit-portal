import { useState } from "react";

interface Props {
  open: boolean;
  taskTitle: string;
  onClose: () => void;
  onConfirm: (comment: string) => void;
}

function SignDialog({ open, taskTitle, onClose, onConfirm }: Props) {
  const [comment, setComment] = useState("");

  if (!open) return null;

  return (
    <div className="dialog-overlay">
      <div className="dialog">
        <h3>חתימה על משימה</h3>
        <p><strong>{taskTitle}</strong></p>

        <textarea
          placeholder="הערה (אופציונלי)"
          value={comment}
          onChange={e => setComment(e.target.value)}
        />

        <div className="dialog-actions">
          <button className="btn-primary" onClick={() => onConfirm(comment)}>חתום</button>
          <button className="btn-ghost" onClick={onClose}>ביטול</button>
        </div>
      </div>
    </div>
  );
}

// styles moved to src/styles/theme.css

export default SignDialog;
