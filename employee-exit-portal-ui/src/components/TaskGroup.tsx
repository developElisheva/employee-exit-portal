import type { ExitTasksGroup } from "../models/exitTasks";
import TaskItem from "./TaskItem";
// styles removed

interface Props {
  group: ExitTasksGroup;
  onSign: (taskId: number, comment: string) => void;
}

function TaskGroup({ group, onSign }: Props) {
  return (
    <div className="card">
      <div className="card-header">
        <div className="card-name">{group.employeeName}</div>
        <div className="card-date">
          תאריך עזיבה: {new Date(group.exitDate).toLocaleDateString("he-IL")}
        </div>
      </div>

      <div className="tasks-list">
        {group.tasks.map(task => (
          <TaskItem
            key={task.taskId}
            task={task}
            onSign={onSign}
          />
        ))}
      </div>
    </div>
  );
}

export default TaskGroup;
