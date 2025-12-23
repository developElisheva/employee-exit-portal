import { useEffect, useState } from "react";
import TaskGroup from "../components/TaskGroup";
import type { ExitFormDetails } from "../models/exitForm";
import { approveTask, getExitFormsForRole } from "../services/exitTasksService";

interface Props {
  department: string;
}

function TasksPage({ department }: Props) {
  const [forms, setForms] = useState<ExitFormDetails[]>([]);
  const [loading, setLoading] = useState(true);

  const loadTasks = async () => {
    setLoading(true);
    try {
      // Fetch ExitFormDetails[] for the current role (server determines role from JWT)
      const forms = await getExitFormsForRole();
      // Use forms directly from API (no mapping)
      setForms(forms || []);
    } catch (err) {
      console.error('Failed loading tasks for', department, err);
      setForms([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadTasks();
  }, [department]);

  const handleSign = async (taskId: number, comment: string) => {
    try {
      await approveTask(taskId, department, comment);
      await loadTasks();
    } catch (err) {
      console.error('Failed approving task', taskId, err);
    }
  };

  if (loading) return <p>טוען משימות…</p>;

  return (
    <div className="page-container">
      <h2 className="page-title">משימות פתוחות – {department}</h2>

      <div className="cards-grid">
        {forms.map(form => (
          <TaskGroup
            key={form.id}
            form={form}
            onSign={handleSign}
          />
        ))}
      </div>
    </div>
  );
}

export default TasksPage;
