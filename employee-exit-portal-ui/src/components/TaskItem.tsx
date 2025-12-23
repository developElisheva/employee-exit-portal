import { useState } from "react";
import SignDialog from "./SignDialog";
import type { ExitTaskDetails } from "../models/exitForm";

interface Props {
  task: ExitTaskDetails;
  onSign: (taskId: number, comment: string) => void;
}

function TaskItem({ task, onSign }: Props) {
  const [openDialog, setOpenDialog] = useState(false);

  const isApproved =
    Boolean(task.isApproved) || Boolean(task.approvedAt) || Boolean(task.approvedBy);

  

  return (
    <li className="task-item">
      <div className="task-info">
        <div className="task-title">{task.topic ?? ''}</div>

        <div className={`task-status ${isApproved ? "approved" : "pending"}`}>
          {isApproved ? "נחתם" : "ממתין"}
        </div>

        {/* מי חתם + תאריך חתימה (ללא שעה) */}
        {isApproved && (task.approvedBy || task.approvedAt) && (
          <div className="task-approved-info">
            {task.approvedBy ? `נחתם ע"י ${task.approvedBy}` : "נחתם"}
            {task.approvedAt && (() => {
              try {
                const d = new Date(task.approvedAt!);
                const date = d.toLocaleDateString("he-IL");
                return ` ב־${date}`;
              } catch {
                return ` ב־${task.approvedAt}`;
              }
            })()}
          </div>
        )}

        {task.comment && (
          <div className="task-note">
            הערה: {task.comment}
          </div>
        )}
      </div>

      <div className="actions">
        {!isApproved && (
          <button
            className="btn-primary"
            onClick={() => setOpenDialog(true)}
          >
            חתימה
          </button>
        )}
        

        <SignDialog
          open={openDialog}
          taskTitle={task.topic ?? ''}
          onClose={() => setOpenDialog(false)}
          onConfirm={(comment) => {
            onSign(task.id, comment);
            setOpenDialog(false);
          }}
        />
      </div>
      
    </li>
  );
}

export default TaskItem;
