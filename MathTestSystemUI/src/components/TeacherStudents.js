import React, { useEffect, useState } from 'react';
import { Container, Card, Table, Spinner, Alert } from 'react-bootstrap';

function TeacherStudents() {
  const [students, setStudents] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetch('http://localhost:5131/api/Teachers/students', {
      headers: {
        'Content-Type': 'application/json'
      },
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error('Failed to fetch students.');
        }
        return response.json();
      })
      .then((data) => {
        setStudents(data);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error:', error);
        setError(error.message || 'An error occurred while fetching data.');
        setLoading(false);
      });
  }, []);

  return (
    <Container className="mt-4">
      <Card>
        <Card.Header as="h3">Students</Card.Header>
        <Card.Body>
          {loading && (
            <div className="text-center my-4">
              <Spinner animation="border" role="status" />
              <span className="ml-2">Loading...</span>
            </div>
          )}

          {error && (
            <Alert variant="danger" className="mt-3">
              {error}
            </Alert>
          )}

          {!loading && !error && students.length > 0 && (
            <Table striped bordered hover responsive className="mt-3">
              <thead>
                <tr>
                  <th>Student ID</th>
                  <th>Exam ID</th>
                  <th>Task ID</th>
                  <th>Task Text</th>
                  <th>Correct Result</th>
                  <th>Student Result</th>
                  <th>Is Correct</th>
                </tr>
              </thead>
              <tbody>
                {students.map((student) =>
                  student.exams.map((exam) =>
                    exam.tasks.map((task) => (
                      <tr key={`${student.id}-${exam.id}-${task.id}`}>
                        <td>{student.id}</td>
                        <td>{exam.id}</td>
                        <td>{task.id}</td>
                        <td>{task.taskText}</td>
                        <td>{task.correctResult}</td>
                        <td>{task.studentResult}</td>
                        <td>{task.isCorrect ? 'Yes' : 'No'}</td>
                      </tr>
                    ))
                  )
                )}
              </tbody>
            </Table>
          )}

          {!loading && !error && students.length === 0 && (
            <Alert variant="info" className="mt-3">
              No students available.
            </Alert>
          )}
        </Card.Body>
      </Card>
    </Container>
  );
}

export default TeacherStudents;
