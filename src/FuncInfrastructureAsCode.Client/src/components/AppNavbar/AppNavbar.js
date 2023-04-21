import React from "react";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';

import { useMsal, useAccount } from "@azure/msal-react";

import "./AppNavbar.css";

const AppNavbar = () => {
  const { accounts, instance } = useMsal();
  const account = useAccount(accounts[0] || {});

  const handleLogout = async () => {
    instance.logoutRedirect({ account: account });
  };

  return (
    <Navbar bg="light" expand="lg">
      <Container>
        <Navbar.Brand href="#home">IaC</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link href="#home">Home</Nav.Link>
            <Nav.Link href="#link">Link</Nav.Link>
          </Nav>
          <Form className="d-flex">
            <Button variant="outline-success" onClick={handleLogout}>
              Logout
            </Button>
          </Form>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

AppNavbar.propTypes = {};

AppNavbar.defaultProps = {};

export default AppNavbar;
