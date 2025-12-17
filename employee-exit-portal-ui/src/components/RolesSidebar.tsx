
interface Props {
  roles: string[];
  activeRole: string | null;
  onSelect: (role: string) => void;
}

export default function RolesSidebar({ roles, activeRole, onSelect }: Props) {
  return (
    <div>
      <h3 className="sidebar-title">תחומים</h3>

      {roles.map(role => (
        <button
          key={role}
          onClick={() => onSelect(role)}
          className={`role-button ${role === activeRole ? 'active' : ''}`}
        >
          {role}
        </button>
      ))}
    </div>
  );
}
