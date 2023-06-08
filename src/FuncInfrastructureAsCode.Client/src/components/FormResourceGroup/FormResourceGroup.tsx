import React from "react";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import "./FormResourceGroup.css";
import { useVirtualMachine } from "../../providers/VirtualMachineProvider";

const FormResourceGroup: React.FC<FormResourceGroupProps> = () => {
  const {
    resourceGroup,
    setResourceGroupName,
    setResourceGroupLocalName,
    setResourceGroupLocation,
  } = useVirtualMachine();

  const handleRgNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setResourceGroupName(event.target.value);
  };

  const handleRgLocalNameChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setResourceGroupLocalName(event.target.value);
  };

  const handleRgLocationChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setResourceGroupLocation(event.target.value);
  };

  return (
    <>
      <h2>ResourceGroup</h2>
      <Row>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.rg.name">
            <Form.Label>Name</Form.Label>
            <Form.Control
              type="text"
              placeholder="name"
              value={resourceGroup.name}
              onChange={handleRgNameChange}
            />
          </Form.Group>
        </Col>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.rg.localName">
            <Form.Label>LocalName</Form.Label>
            <Form.Control
              type="text"
              placeholder="localName"
              value={resourceGroup.localName}
              onChange={handleRgLocalNameChange}
            />
          </Form.Group>
        </Col>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.rg.location">
            <Form.Label>Location</Form.Label>
            <Form.Control
              type="text"
              placeholder="location"
              value={resourceGroup.location}
              onChange={handleRgLocationChange}
            />
          </Form.Group>
        </Col>
      </Row>
    </>
  );
};

FormResourceGroup.propTypes = {};

FormResourceGroup.defaultProps = {};

interface FormResourceGroupProps {
}

export default FormResourceGroup;
