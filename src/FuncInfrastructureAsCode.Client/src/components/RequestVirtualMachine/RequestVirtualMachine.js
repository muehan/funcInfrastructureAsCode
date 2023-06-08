import React from "react";
import Form from "react-bootstrap/Form";
import Container from "react-bootstrap/Container";
import Button from "react-bootstrap/Button";
import FormResourceGroup from "../FormResourceGroup/FormResourceGroup";
import FormVirtualNetwork from "../FormVirtualNetwork/FormVirtualNetwork";
import FormSubnet from "../FormSubnet/FormSubnet";
import FormNetworkInterface from "../FormNetworkInterface/FormNetworkInterface";
import { useVirtualMachine } from "../../providers/VirtualMachineProvider";

import "./RequestVirtualMachine.css";
import FormVirtualMachine from "../FormVirtualMachine/FormVirtualMachine";

const RequestVirtualMachine = () => {
  const {
    resourceGroup,
    virtualNetwork,
  } = useVirtualMachine();

  const handleSubmit = (event) => {
    alert("A name was submitted: " + resourceGroup.name);
    console.log(resourceGroup);
    console.log(virtualNetwork.name);
    event.preventDefault();
  };

  return (
    <Container fluid>
      <Form onSubmit={handleSubmit}>
        <FormResourceGroup />
        <hr />
        <FormVirtualNetwork />
        <hr />
        <FormSubnet />
        <hr />
        <FormNetworkInterface />
        <hr />
        <FormVirtualMachine />
        <hr />
        <Button variant="primary" type="submit">
          Submit
        </Button>
      </Form>
    </Container>
  );
};

RequestVirtualMachine.propTypes = {};

RequestVirtualMachine.defaultProps = {};

export default RequestVirtualMachine;
