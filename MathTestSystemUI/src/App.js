// src/App.js
import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import TeacherUpload from './components/TeacherUpload';
import TeacherStudents from './components/TeacherStudents';
import StudentDetails from './components/StudentDetails';
import { Navbar, Nav, Container } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <Router>
      <Navbar bg="dark" variant="dark" expand="lg">
        <Container>
          <Navbar.Brand as={Link} to="/">
            MathTestSystem
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link as={Link} to="/teacher/upload">
                Upload Exams
              </Nav.Link>
              <Nav.Link as={Link} to="/teacher/students">
                View Students
              </Nav.Link>
              <Nav.Link as={Link} to="/student/">
                Student Details
              </Nav.Link>
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>

      <Container className="mt-4">
        <Routes>
          <Route path="/teacher/upload" element={<TeacherUpload />} />
          <Route path="/teacher/students" element={<TeacherStudents />} />
          <Route path="/student/" element={<StudentDetails />} />
          <Route path="/" element={<Home />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Container>
    </Router>
  );
}

function Home() {
  return (
    <div className="text-center">
      <h1>Welcome to MathTestSystem</h1>
      <p>Select an option from the navigation bar to get started.</p>
    </div>
  );
}

function NotFound() {
  return (
    <div className="text-center">
      <h2>404 - Page Not Found</h2>
      <p>The page you're looking for doesn't exist.</p>
    </div>
  );
}

export default App;
