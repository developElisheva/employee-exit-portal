import type { ExitFormDetails } from "../models/exitForm";
import TaskItem from "./TaskItem";
// styles removed

interface Props {
  form: ExitFormDetails;
  onSign: (taskId: number, comment: string) => void;
}

function TaskGroup({ form, onSign }: Props) {
  const dateStr = form.endDate ?? (form as any).exitDate ?? '';

  return (
    <div className="card">
      <div className="card-header">
        <div className="card-name">{form.employeeName}</div>
        <div className="card-date">
          תאריך עזיבה: {dateStr ? new Date(dateStr).toLocaleDateString("he-IL") : '-'}
        </div>
      </div>

      <div className="tasks-list">
        {form.tasks.map(task => (
          <TaskItem
            key={(task as any).taskId ?? (task as any).id}
            task={task}
            onSign={onSign}
          />
        ))}
      </div>
    </div>
  );
}

export default TaskGroup;
