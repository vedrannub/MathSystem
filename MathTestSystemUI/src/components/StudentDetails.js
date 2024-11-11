import React, { useState } from 'react';
import { Tabs, Tab, Form, Button, Table, Alert, Spinner } from 'react-bootstrap';

function StudentDetails() {
  const [studentId, setStudentId] = useState('');
  const [exams, setExams] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const handleInputChange = (e) => {
    setStudentId(e.target.value);
  };

  const handleFetchExams = () => {
    if (!studentId) {
      setError('Please enter a student ID.');
      return;
    }

    setLoading(true);
    setError(null);
    setExams([]);

    fetch(`http://localhost:5131/api/Students/${studentId}/exams`)
      .then((response) => {
        if (!response.ok) {
          throw new Error('Student or exams not found.');
        }
        return response.json();
      })
      .then((data) => {
        setExams(data);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error:', error);
        setError(error);
        setLoading(false);
      });
  };

  return (
    <div className="container mt-4">
      <h2>Student Details</h2>
      <Tabs defaultActiveKey="search" id="student-tabs" className="mb-3">
        <Tab eventKey="search" title="Search Student">
          <Form>
            <Form.Group controlId="formStudentId">
              <Form.Label>Student ID</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter Student ID"
                value={studentId}
                onChange={handleInputChange}
              />
            </Form.Group>
            <Button variant="primary" className="mt-2" onClick={handleFetchExams}>
              Get Exams
            </Button>
          </Form>

          {loading && (
            <div className="mt-3">
              <Spinner animation="border" role="status" />
              <span className="ml-2">Loading...</span>
            </div>
          )}

          {error && (
            <Alert variant="danger" className="mt-3">
              {error.message || error}
            </Alert>
          )}

          {!loading && !error && exams.length > 0 && (
            <div className="mt-4">
              <h3>Exams for Student ID: {studentId}</h3>
              {exams.map((exam) => (
                <div key={exam.id} className="mb-4">
                  <h4>Exam ID: {exam.id}</h4>
                  <Table striped bordered hover>
                    <thead>
                      <tr>
                        <th>Task ID</th>
                        <th>Task Text</th>
                        <th>Correct Result</th>
                        <th>Student Result</th>
                        <th>Is Correct</th>
                      </tr>
                    </thead>
                    <tbody>
                      {exam.tasks.map((task) => (
                        <tr key={task.id}>
                          <td>{task.id}</td>
                          <td>{task.taskText}</td>
                          <td>{task.correctResult}</td>
                          <td>{task.studentResult}</td>
                          <td>{task.isCorrect ? 'Yes' : 'No'}</td>
                        </tr>
                      ))}
                    </tbody>
                  </Table>
                </div>
              ))}
            </div>
          )}

          {!loading && !error && exams.length === 0 && studentId && (
            <Alert variant="info" className="mt-3">
              No exams available for this student.
            </Alert>
          )}
        </Tab>
      </Tabs>
    </div>
  );
}

export default StudentDetails;
