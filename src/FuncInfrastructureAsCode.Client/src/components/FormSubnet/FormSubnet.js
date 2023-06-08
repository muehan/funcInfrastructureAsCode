import React from "react";
import { useVirtualMachine } from "../../providers/VirtualMachineProvider";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import "./FormSubnet.css";

const FormSubnet = () => {

  const {
    subnet,
    setSubnetName,
    setSubnetAddressPrefixes,
  } = useVirtualMachine();

  const handleSnNameChange = (event) => {
    setSubnetName(event.target.value);
  };

  const handleAndressPrefixesChange = (event) => {
    setSubnetAddressPrefixes(event.target.value);
  };

  return (
    <>
      <h2>Subnet</h2>

      <Row>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vn.name">
            <Form.Label>Name</Form.Label>
            <Form.Control
              type="text"
              placeholder="name"
              value={subnet.name}
              onChange={handleSnNameChange}
            />
          </Form.Group>
        </Col>

        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vn.name">
            <Form.Label>AddressPrefixes</Form.Label>
            <Form.Control
              type="text"
              placeholder="addressPrefixes"
              value={subnet.addressPrefixes}
              onChange={handleAndressPrefixesChange}
            />
          </Form.Group>
        </Col>
      </Row>

      <Row>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vn.name">
            <Form.Label>ResourceGroupName</Form.Label>
            <Form.Control
              type="text"
              placeholder="ResourceGroupName"
              value={subnet.resourceGroupName}
              disabled
            />
          </Form.Group>
        </Col>

        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vn.localName">
            <Form.Label>LocalName</Form.Label>
            <Form.Control
              type="text"
              placeholder="localName"
              value={subnet.localName}
              disabled
            />
          </Form.Group>
        </Col>

        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vn.location">
            <Form.Label>VirtualNetworkName</Form.Label>
            <Form.Control
              type="text"
              placeholder="VirtualNetworkName"
              value={subnet.virtualNetworkName}
              disabled
            />
          </Form.Group>
        </Col>
      </Row>
    </>
  );
};

FormSubnet.propTypes = {};

FormSubnet.defaultProps = {};

export default FormSubnet;
