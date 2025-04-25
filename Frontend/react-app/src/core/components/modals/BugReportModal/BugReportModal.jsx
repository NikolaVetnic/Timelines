import axios from "axios";
import { useState } from "react";
import { toast } from "react-toastify";
import Button from "../../buttons/Button/Button";
import FormField from "../../forms/FormField/FormField";
import "./BugReportModal.css";

const apiUrl = "http://localhost/api";
const exactPath = "/BugReports";

const BugReportModal = ({ isOpen, onClose }) => {
    const [bugReport, setBugReport] = useState({
        title: "",
        description: "",
        reporterName: "Pavel Jurišiċ",
    });
    const [errors, setErrors] = useState({});
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleBugReportChange = (e) => {
        const { name, value } = e.target;
        setBugReport((prev) => ({ ...prev, [name]: value }));
        if (errors[name]) {
            setErrors((prev) => ({ ...prev, [name]: "" }));
        }
    };

    const validateForm = () => {
        const newErrors = {};
        if (!bugReport.title.trim()) newErrors.title = "Title is required";
        if (!bugReport.description.trim())
            newErrors.description = "Description is required";
        if (!bugReport.reporterName.trim())
            newErrors.reporterName = "Your name is required";

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleBugReportSubmit = async (e) => {
        e.preventDefault();

        if (!validateForm()) return;

        setIsSubmitting(true);

        const data = {
            title: bugReport.title,
            description: bugReport.description,
            reporterName: bugReport.reporterName,
        };

        try {
            await axios.post(`${apiUrl}${exactPath}`, data, {
                headers: {
                    "Content-Type": "application/json",
                    Accept: "application/json",
                },
            });

            toast.success("Bug report submitted successfully!");
            onClose();
            setBugReport({ title: "", description: "", reporterName: "" });
        } catch (error) {
            toast.error(
                error.response?.data?.message || "Failed to submit bug report"
            );
        } finally {
            setIsSubmitting(false);
        }
    };

    if (!isOpen) return null;

    return (
        <div className="bug-report-modal-overlay">
            <div className="bug-report-modal-content">
                <div className="bug-report-modal-header">
                    <h3>Report a Bug</h3>
                    <p>Help us improve by reporting any issues you encounter</p>
                </div>

                <form onSubmit={handleBugReportSubmit}>
                    <FormField
                        label="Title"
                        type="text"
                        name="title"
                        value={bugReport.title}
                        onChange={handleBugReportChange}
                        placeholder="Brief description of the issue"
                        required
                        disabled={isSubmitting}
                        error={errors.title}
                    />

                    <FormField
                        label="Your Name"
                        type="text"
                        name="reporterName"
                        value={bugReport.reporterName}
                        onChange={handleBugReportChange}
                        placeholder="How should we call you?"
                        required
                        disabled={isSubmitting}
                        error={errors.reporterName}
                    />

                    <FormField
                        label="Description"
                        type="textarea"
                        name="description"
                        value={bugReport.description}
                        onChange={handleBugReportChange}
                        placeholder="Please describe the bug in detail..."
                        required
                        disabled={isSubmitting}
                        error={errors.description}
                    />

                    <div className="bug-report-modal-actions">
                        <Button
                            type="button"
                            onClick={onClose}
                            disabled={isSubmitting}
                            variant="secondary"
                            text="Cancel"
                        />
                        <Button
                            type="submit"
                            disabled={isSubmitting}
                            variant="primary"
                            text={
                                isSubmitting ? "Submitting..." : "Submit Report"
                            }
                        />
                    </div>
                </form>
            </div>
        </div>
    );
};

export default BugReportModal;
