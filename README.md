# MathTestSystem

## Features

- **Upload Exams:** Easily upload XML files containing comprehensive exam data.
- **View Students:** Access a detailed list of students along with their respective exam performances.
- **Student Details:** Search for individual students to view in-depth exam results and performance metrics.
- **Secure API:** Ensure data integrity and security with protected API endpoints using API key authentication.
- **Responsive Design:** Enjoy a user-friendly interface optimized for various devices and screen sizes.

## Screenshots

![quipu1](https://github.com/user-attachments/assets/e30470c5-7d97-4170-a544-1e178a5a2a70)

![quipu2](https://github.com/user-attachments/assets/81be7592-fa17-4843-a1b5-4e865523a8f1)

![quipu3](https://github.com/user-attachments/assets/228e7cbf-09a9-4280-8c2b-ea098d19c60b)

![quipu4](https://github.com/user-attachments/assets/8a91375e-6cb9-4ec5-bfa2-3b30c9cd305f)

![quipu5](https://github.com/user-attachments/assets/5be46e51-5ce0-44bc-8224-979605db5f0b)

## Future Improvements

- Authorization/Authentication with JWT
- Global Error Handler
- Logger
- SQL Base - Using in-memory DB for this case (The relations are defined in the AppDbContext)
- Integration Service Limitations

Example XML:

<Teacher ID = "11111">
    
    <Students>
    
        <Student ID="12345">
    
            <Exam ID="1">
    
                <Task ID = "1"> 2+3/6-4 = 74 </Task>
    
                <Task ID = "2"> 6*2+3-4 = 22 </Task>
    
            </Exam>
    
        </Student>
    
        <Student ID="54321">
    
            <Exam ID="1">
    
                <Task ID = "1"> 2+3/6-4 = 74 </Task>
    
                <Task ID = "2"> 6*2+3-4 = 22 </Task>
    
            </Exam>
    
        </Student>
    
    </Students>

</Teacher>
