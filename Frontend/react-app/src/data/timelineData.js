const timelineData = [
  {
    id: "1",
    title: "Project Kickoff",
    description:
      "The project was initiated with initial meetings and planning sessions.",
    timestamp: new Date(2023, 0, 1),
    importance: 5,
    phase: "Initiation",
    categories: ["Planning", "Kickoff"],
    tags: ["project-start", "planning"],
    image: "/assets/images/kickoff.jpg",
  },
  {
    id: "2",
    title: "Design Phase",
    description: "Designs were created and approved by stakeholders.",
    timestamp: new Date(2023, 1, 1),
    importance: 4,
    phase: "Design",
    categories: ["UI/UX", "Approvals"],
    tags: ["design-phase", "stakeholders"],
    image: "/assets/images/design.jpg",
  },
  {
    id: "3",
    title: "Development",
    description:
      "The development team started building the application features.",
    timestamp: new Date(2023, 2, 1),
    importance: 5,
    phase: "Development",
    categories: ["Backend", "Frontend"],
    tags: ["development-phase", "coding"],
    image: "/assets/images/development.jpg",
  },
  {
    id: "4",
    title: "Testing",
    description:
      "Quality assurance and testing were performed to ensure product quality.",
    timestamp: new Date(2023, 3, 1),
    importance: 4,
    phase: "Testing",
    categories: ["QA", "Bug Fixes"],
    tags: ["testing-phase", "quality-assurance"],
    image: "/assets/images/testing.jpg",
  },
  {
    id: "5",
    title: "Deployment",
    description: "The application was deployed to the production environment.",
    timestamp: new Date(2023, 4, 1),
    importance: 5,
    phase: "Deployment",
    categories: ["Release", "Production"],
    tags: ["deployment-phase", "production"],
    image: "/assets/images/deployment.jpg",
  },
];

export default timelineData;
