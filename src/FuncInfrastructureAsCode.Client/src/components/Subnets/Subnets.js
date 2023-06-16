import React from "react";
import "./Subnets.css";
import Table from "react-bootstrap/Table";
import { useFetch } from "../../apis/FetchData";

const Subnets = (props) => {
  const url = props.requestId ? "/Subnets/" + props.requestId : "/Subnets";

  const subnets = useFetch(url);

  if (!Array.isArray(subnets.response)) {
    return <p>Loading...</p>;
  }

  const data = subnets.response;

  return (
    <div>
      <h3>Subnets</h3>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Name</th>
            <th>Localname</th>
            <th>ResourceGroupName</th>
            <th>AddressPrefixes</th>
            <th>VirtualNetworkName</th>
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
                  <td>{row.resourceGroupName}</td>
                  <td>{row.addressPrefixes}</td>
                  <td>{row.virtualNetworkName}</td>
                  <td>{row.status}</td>
                </tr>
              );
            })}
        </tbody>
      </Table>
    </div>
  );
};

Subnets.propTypes = {};

Subnets.defaultProps = {};

export default Subnets;
