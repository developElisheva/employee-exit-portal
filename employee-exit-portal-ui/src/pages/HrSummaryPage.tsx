import { useState } from "react";
import HrExitFormsListPage from "./HrExitFormsListPage.tsx";
import HrExitFormDetailsPage from "./HrExitFormDetailsPage.tsx";

export default function HrSummaryPage() {
  const [selectedFormId, setSelectedFormId] = useState<number | null>(null);

  if (selectedFormId !== null) {
    return (
      <HrExitFormDetailsPage
        formId={selectedFormId}
        onBack={() => setSelectedFormId(null)}
      />
    );
  }

  return (
    <HrExitFormsListPage onSelectForm={setSelectedFormId} />
  );
}
