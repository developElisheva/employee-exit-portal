import { useEffect, useState } from "react";
import { getRoles } from "./services/exitTasksService";
import TasksPage from "./pages/TasksPage";
import MainLayout from "./layouts/MainLayout";
import RolesSidebar from "./components/RolesSidebar";

function App() {
  const [roles, setRoles] = useState<string[]>([]);
  const [activeRole, setActiveRole] = useState<string | null>(null);

  useEffect(() => {
    getRoles().then(r => {
      setRoles(r);
      setActiveRole(r[0]);
    });
  }, []);

  return (
    <MainLayout
      sidebar={<RolesSidebar roles={roles} activeRole={activeRole} onSelect={(r) => setActiveRole(r)} />}
    >
      {activeRole && <TasksPage role={activeRole} />}
    </MainLayout>
  );
}

export default App;
