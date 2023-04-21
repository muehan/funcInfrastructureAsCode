import "./NetworkInterfaces.css";
import React from "react";
import Table from "react-bootstrap/Table";
import { useNetworkInterfaces } from "./../../apis/FetchNetworkInterfaces";


const NetworkInterfaces = () => {
  const networkInterfaces = useNetworkInterfaces();

  if (!Array.isArray(networkInterfaces.response)) {
    return <p>Loading...</p>;
  }

  const data = networkInterfaces.response;

  return (
    <Table striped bordered hover>
      <thead>
        <tr>
          <th>Name</th>
          <th>Localname</th>
          <th>Location</th>
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
              </tr>
            )
          })}
      </tbody>
    </Table>
  )
};

NetworkInterfaces.propTypes = {};

NetworkInterfaces.defaultProps = {};

export default NetworkInterfaces;
