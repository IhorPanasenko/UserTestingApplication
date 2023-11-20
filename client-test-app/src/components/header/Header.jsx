import React from 'react'
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

function Header() {
    return (
        <Navbar className='fs-2' bg="dark" data-bs-theme="dark">
            <Container>
                <Navbar.Brand className='fs-2 m-auto' href="#home">TestsApp</Navbar.Brand>
                <Nav className="m-auto">
                    <Nav.Link className='m-2 p-4 pb-0 pt-0' href="/">Login</Nav.Link>
                    <Nav.Link className='m-2 p-4 pb-0 pt-0' href="/UserTests">My tests</Nav.Link>
                    <Nav.Link className='m-2 p-4 pb-0 pt-0' href="/About">About</Nav.Link>
                </Nav>
            </Container>
        </Navbar>
    )
}

export default Header