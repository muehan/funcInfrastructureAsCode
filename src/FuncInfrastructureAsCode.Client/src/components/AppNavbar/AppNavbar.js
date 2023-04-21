import React from "react";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import NavDropdown from "react-bootstrap/NavDropdown";

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
            <Nav.Link href="/">Home</Nav.Link>

            <NavDropdown title="Resources" id="basic-nav-dropdown">
              <NavDropdown.Item href="/resourceGroups">
                ResourceGroup
              </NavDropdown.Item>
              <NavDropdown.Item href="/networkInterfaces">
                NetworkInterfaces
              </NavDropdown.Item>
              <NavDropdown.Item href="/subnets">Subnets</NavDropdown.Item>
              <NavDropdown.Item href="/virtualNetowrk">
                VirtualNetworks
              </NavDropdown.Item>
              <NavDropdown.Item href="#action/3.3">
                VirtualMachines
              </NavDropdown.Item>
            </NavDropdown>
          </Nav>
          <Form className="d-flex">
            <Button variant="outline-dark" onClick={handleLogout}>
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
