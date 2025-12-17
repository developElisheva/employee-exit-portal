import { useState } from "react";
import SignDialog from "./SignDialog";
import type { ExitTask } from "../models/exitTasks";

interface Props {
  task: ExitTask;
  onSign: (taskId: number, comment: string) => void;
}

function TaskItem({ task, onSign }: Props) {
  const [openDialog, setOpenDialog] = useState(false);
  return (
    <li className="task-item">
      <div className="task-info">
        <div className="task-title">{task.title}</div>
        <div className={`task-status ${task.status === "Approved" ? 'approved' : 'pending'}`}>
          {task.status === "Approved" ? "נחתם" : "ממתין"}
        </div>

        {task.comments && (
          <div className="task-note">
            הערה: {task.comments}
          </div>
        )}
      </div>

      <div className="actions">
        {task.status !== "Approved" && (
          <button className="btn-primary" onClick={() => setOpenDialog(true)}>
            חתימה
          </button>
        )}

        <SignDialog
          open={openDialog}
          taskTitle={task.title}
          onClose={() => setOpenDialog(false)}
          onConfirm={(comment) => {
            onSign(task.taskId, comment);
            setOpenDialog(false);
          }}
        />
      </div>
    </li>
  );
}

export default TaskItem;
