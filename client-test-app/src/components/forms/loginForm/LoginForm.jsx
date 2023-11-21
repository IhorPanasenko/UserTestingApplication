import React, { useState } from 'react';
import axios from 'axios';
import { Form, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

const URL_LOGIN = 'http://localhost:5058/api/Authentication/Login';

function LoginForm() {
    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
    const [showPassword, setShowPassword] = useState(false);
    const navigate = useNavigate();
    localStorage.clear();

    const handleLoginChange = (event) => {
        setLogin(event.target.value);
    };

    const handlePasswordChange = (event) => {
        setPassword(event.target.value);
    };

    const handleShowPassword = () => {
        setShowPassword(!showPassword);
    };

    const handleLogin = async () => {
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$/;
        if (!password.match(passwordRegex)) {
            alert('Password must have at least:\n1 uppercase letter,\n1 lowercase letter,\n1 digit,\n1 special symbol,\nand minimum length of 6.');
            return;
        }

        const requestData = {
            login: login,
            password: password,
        };

        try {
            const response = await axios.post(URL_LOGIN, requestData);
            console.log('API response:', response.data);
            localStorage.setItem("token", response.data);
            alert("Congratulation! Successfull login");
            navigate("UserTests");

        } catch (error) {
            console.error('API error:', error);

            console.log(error.response);
            if (error.response !== undefined) {
                alert(error.response.data);
            }
            else {
                alert("Internal Server error")
            }
        }
    };

    return (
        <div className="mt-5 bg-warning p-3 border border-rounded container">
            <Form className='fs-2'>
                <Form.Group controlId="login">
                    <Form.Label>Login</Form.Label>
                    <Form.Control className='fs-2' type="text" placeholder="Enter login" value={login} onChange={handleLoginChange} />
                </Form.Group>
                <Form.Group controlId="password">
                    <Form.Label>Password</Form.Label>
                    <Form.Control className='fs-2' type={showPassword ? 'text' : 'password'} placeholder="Password" value={password} onChange={handlePasswordChange} />
                    <Form.Check type="checkbox" label="Show password" onChange={handleShowPassword} />
                </Form.Group>
                <div className="d-flex w-100 justify-content-center">
                    <Button className='fs-2 m-2 p-5 pt-2 pb-2' variant="primary" onClick={handleLogin}>
                        Login
                    </Button>
                </div>
            </Form>
        </div>
    );
}

export default LoginForm;