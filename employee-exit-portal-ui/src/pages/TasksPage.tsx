import { useEffect, useState } from "react";
import TaskGroup from "../components/TaskGroup";
import type { ExitTasksGroup } from "../models/exitTasks";
import { approveTask, getGroupedTasksByRole } from "../services/exitTasksService";
// styles removed

interface Props {
  role: string;
}

function TasksPage({ role }: Props) {
  const [groups, setGroups] = useState<ExitTasksGroup[]>([]);
  const [loading, setLoading] = useState(true);

  const loadTasks = async () => {
    setLoading(true);
    const data = await getGroupedTasksByRole(role);
    setGroups(data);
    setLoading(false);
  };

  useEffect(() => {
    loadTasks();
  }, [role]);

  const handleSign = async (taskId: number, comment: string) => {
    await approveTask(taskId, role, comment);
    await loadTasks();
  };

  if (loading) return <p>טוען משימות…</p>;

  return (
    <div className="page-container">
      <h2 className="page-title">משימות פתוחות – {role}</h2>

      <div className="cards-grid">
        {groups.map(group => (
          <TaskGroup
            key={group.exitFormId}
            group={group}
            onSign={handleSign}
          />
        ))}
      </div>
    </div>
  );
}

export default TasksPage;
