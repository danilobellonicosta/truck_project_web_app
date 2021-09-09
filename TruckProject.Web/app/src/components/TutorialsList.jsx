import React, { useState, useEffect } from "react";
import TruckDataService from "../services/TruckService";
import { Link } from "react-router-dom";
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/Delete';

const TruckList = () => {
  const [trucks, setTrucks] = useState([]);
  const [message, setMessage] = useState("");

  useEffect(() => {
    retrieveTrucks();
  }, []);

  const retrieveTrucks = () => {
    TruckDataService.getAll()
      .then(response => {
        setTrucks(response.data);
        console.log(response.data);
      })
      .catch(e => {
        setMessage("Error loading data: " + e.response?.data?.errors);
      });
  };

  const refreshList = () => {
    retrieveTrucks();
  };

  const remove = (id) => {
    TruckDataService.remove(id)
      .then(response => {
        refreshList();
      })
      .catch(e => {
        setMessage("Error deleting record: " + e.response?.data?.errors);
      });
  };

  return (
    <TableContainer component={Paper}>
      <Table className="" size="small" aria-label="a dense table">
        <TableHead>
          <TableRow>
            <TableCell>Truck Id</TableCell>
            <TableCell align="right">Fabrication Year</TableCell>
            <TableCell align="right">Chassi</TableCell>
            <TableCell align="right">Model Year</TableCell>
            <TableCell align="right">Edit</TableCell>
            <TableCell align="right">Delete</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {trucks.map((row) => (
            <TableRow key={row.id}><TableCell component="th" scope="row">{row.id}</TableCell>
              <TableCell align="right">{row.fabricationYear}</TableCell>
              <TableCell align="right">{row.chassi}</TableCell>
              <TableCell align="right">{row.modelYear}</TableCell>
              <TableCell align="right">
                  <Link to={"/edit/" + row.id} className="nav-link"><EditIcon />
                </Link>
            </TableCell>
              <TableCell align="right"><DeleteIcon  onClick={() => remove(row.id)}/></TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default TruckList;