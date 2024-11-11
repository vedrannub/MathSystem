// src/components/TeacherUpload.js
import React, { useState } from 'react';
import { Form, Button, Alert, Spinner, Card, Container } from 'react-bootstrap';

function TeacherUpload() {
  const [file, setFile] = useState(null);
  const [message, setMessage] = useState({ type: '', text: '' });
  const [loading, setLoading] = useState(false);

  const handleFileChange = (e) => {
    setFile(e.target.files[0]);
    setMessage({ type: '', text: '' }); // Reset message on new file selection
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!file) {
      setMessage({ type: 'danger', text: 'Please select a valid XML file.' });
      return;
    }

    setLoading(true);
    setMessage({ type: '', text: '' });

    const formData = new FormData();
    formData.append('xmlFile', file);

    try {
      const response = await fetch('http://localhost:5131/api/Exams/upload', {
        method: 'POST',
        body: formData,
        headers: {
          // Include API Key if your endpoint requires authentication
          'ApiKey': 'Your_Secure_API_Key', // Replace with your actual API key
        },
      });

      if (response.ok) {
        setMessage({ type: 'success', text: 'File processed successfully.' });
      } else {
        const errorData = await response.json();
        setMessage({ type: 'danger', text: errorData.message || 'An error occurred while processing the file.' });
      }
    } catch (error) {
      console.error('Error:', error);
      setMessage({ type: 'danger', text: 'An error occurred while processing the file.' });
    } finally {
      setLoading(false);
      setFile(null); // Reset the file input
    }
  };

  return (
    <Container className="mt-4">
      <Card>
        <Card.Header as="h3">Upload Exams</Card.Header>
        <Card.Body>
          <Form onSubmit={handleSubmit}>
            <Form.Group controlId="formFile" className="mb-3">
              <Form.Label>Select XML File</Form.Label>
              <Form.Control
                type="file"
                accept=".xml"
                onChange={handleFileChange}
              />
            </Form.Group>
            <Button variant="primary" type="submit" disabled={loading}>
              {loading ? (
                <>
                  <Spinner
                    as="span"
                    animation="border"
                    size="sm"
                    role="status"
                    aria-hidden="true"
                  />{' '}
                  Uploading...
                </>
              ) : (
                'Upload'
              )}
            </Button>
          </Form>

          {message.text && (
            <Alert variant={message.type} className="mt-3">
              {message.text}
            </Alert>
          )}
        </Card.Body>
      </Card>
    </Container>
  );
}

export default TeacherUpload;
