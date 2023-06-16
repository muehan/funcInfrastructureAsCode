import React from "react";
import "./VirtualMachines.css";
import Table from "react-bootstrap/Table";
import { useFetch } from "../../apis/FetchData";

const VirtualMachines = (props) => {
  const url = props.requestId
    ? "/VirtualMachines/" + props.requestId
    : "/VirtualMachines";

  const virtualMachines = useFetch(url);

  if (!Array.isArray(virtualMachines.response)) {
    return <p>Loading...</p>;
  }

  const data = virtualMachines.response;

  return (
    <div>
      <h3>NetworkInterfaces</h3>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Name</th>
            <th>Localname</th>
            <th>Location</th>
            <th>ResourceGroupName</th>
            <th>Size</th>
            <th>OsDiskCachine</th>
            <th>OsDiskStorageAccountType</th>
            <th>SourceImageReferenceOffer</th>
            <th>SourceImageReferencePublisher</th>
            <th>SourceImageReferenceSku</th>
            <th>SourceImageReferenceVersion</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {data &&
            data.map((row) => {
              return (
                <tr key={row.name}>
                  <td>{row.name}</td>
                  <td>{row.localName}</td>
                  <td>{row.location}</td>
                  <td>{row.resourceGroupName}</td>
                  <td>{row.size}</td>
                  <td>{row.osDiskCachine}</td>
                  <td>{row.osDiskStorageAccountType}</td>
                  <td>{row.sourceImageReferenceOffer}</td>
                  <td>{row.sourceImageReferencePublisher}</td>
                  <td>{row.sourceImageReferenceSku}</td>
                  <td>{row.sourceImageReferenceVersion}</td>
                  <td>{row.status}</td>
                </tr>
              );
            })}
        </tbody>
      </Table>
    </div>
  );
};

VirtualMachines.propTypes = {};

VirtualMachines.defaultProps = {};

export default VirtualMachines;
