import { useState } from "react";
import axios from "axios";
import Modal from "./Modal/Modal";
import { toast } from "react-toastify";
import "./BugReportModal.css";

const apiUrl = "http://localhost/api";
const exactPath = "/BugReports";

const BugReportModal = ({ setIsBugReportOpen }) => {
    const [bugReport, setBugReport] = useState({
        title: "",
        description: "",
        reporterName: "Pavle Jurišić",
    });
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleBugReportChange = (e) => {
        const { name, value } = e.target;
        setBugReport((prev) => ({ ...prev, [name]: value }));
    };

    const handleBugReportSubmit = async (e) => {
        e.preventDefault();
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
            setIsBugReportOpen(false);
            setBugReport({ title: "", description: "", reporterName: "" });
        } catch (error) {
            toast.error(
                error.response?.data?.message || "Failed to submit bug report"
            );
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <Modal onClose={() => !isSubmitting && setIsBugReportOpen(false)}>
            <div className="bug-report-modal">
                <div className="modal-header">
                    <h2>Report a Bug</h2>
                    <p>Help us improve by reporting any issues you encounter</p>
                </div>

                <form onSubmit={handleBugReportSubmit}>
                    <div className="form-group">
                        <label htmlFor="title">Title*</label>
                        <input
                            type="text"
                            id="title"
                            name="title"
                            value={bugReport.title}
                            onChange={handleBugReportChange}
                            placeholder="Brief description of the issue"
                            required
                            disabled={isSubmitting}
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="reporterName">Your Name*</label>
                        <input
                            type="text"
                            id="reporterName"
                            name="reporterName"
                            value={bugReport.reporterName}
                            onChange={handleBugReportChange}
                            placeholder="How should we call you?"
                            required
                            disabled={isSubmitting}
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="description">Description*</label>
                        <textarea
                            id="description"
                            name="description"
                            value={bugReport.description}
                            onChange={handleBugReportChange}
                            rows={5}
                            placeholder="Please describe the bug in detail..."
                            required
                            disabled={isSubmitting}
                        />
                    </div>

                    <div className="form-actions">
                        <button
                            type="button"
                            onClick={() => setIsBugReportOpen(false)}
                            disabled={isSubmitting}
                            className="cancel-btn"
                        >
                            Cancel
                        </button>
                        <button
                            type="submit"
                            disabled={isSubmitting}
                            className="submit-btn"
                        >
                            {isSubmitting ? "Submitting..." : "Submit Report"}
                        </button>
                    </div>
                </form>
            </div>
        </Modal>
    );
};

export default BugReportModal;
