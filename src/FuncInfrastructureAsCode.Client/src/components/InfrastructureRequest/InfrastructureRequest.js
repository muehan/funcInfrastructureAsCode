import React from "react";
import { useParams } from "react-router";
import ResourceGroups from "../ResourceGroups/ResourceGroups";
import "./InfrastructureRequest.css";
import NetworkInterfaces from "../NetworkInterfaces/NetworkInterfaces";
import Subnets from "../Subnets/Subnets";
import VirtualNetwork from "../VirtualNetwork/VirtualNetwork";
import VirtualMachines from "../VirtualMachines/VirtualMachines";

const InfrastructureRequest = () => {
  const { rowkey } = useParams();

  return (
    <div className="InfrastructureRequest">
      <h1>InfrastructureRequest</h1>
      <p>Rowkey: {rowkey}</p>

      <ResourceGroups requestId={rowkey}></ResourceGroups>
      <NetworkInterfaces requestId={rowkey}></NetworkInterfaces>
      <Subnets requestId={rowkey}></Subnets>
      <VirtualNetwork requestId={rowkey}></VirtualNetwork>
      <VirtualMachines requestId={rowkey}></VirtualMachines>
    </div>
  );
};

InfrastructureRequest.propTypes = {};

InfrastructureRequest.defaultProps = {};

export default InfrastructureRequest;
