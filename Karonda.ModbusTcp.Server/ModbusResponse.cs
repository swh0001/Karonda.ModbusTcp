using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Karonda.ModbusTcp.Entity;
using Karonda.ModbusTcp.Entity.Function.Request;
using Karonda.ModbusTcp.Entity.Function.Response;

namespace Karonda.ModbusTcp.Server
{
    public class ModbusResponse : ModbusResponseService
    {
        public override ModbusFunction ReadCoils(ReadCoilsRequest request)
        {
            var coilArray = ReadCoilsOrInputs(request.Quantity);
            var response = new ReadCoilsResponse(coilArray);

            return response;
        }

        public override ModbusFunction ReadDiscreteInputs(ReadDiscreteInputsRequest request)
        {
            var inputArray = ReadCoilsOrInputs(request.Quantity);
            var response = new ReadDiscreteInputsResponse(inputArray);

            return response;
        }

        public override ModbusFunction ReadHoldingRegisters(ReadHoldingRegistersRequest request)
        {
            var registers = ReadRegisters(request.Quantity);
            var response = new ReadHoldingRegistersResponse(registers);

            return response;
        }

        public override ModbusFunction ReadInputRegisters(ReadInputRegistersRequest request)
        {
            var registers = ReadRegisters(request.Quantity);
            var response = new ReadInputRegistersResponse(registers);

            return response;
        }

        public override ModbusFunction WriteSingleCoil(WriteSingleCoilRequest request)
        {
            var response = new WriteSingleCoilResponse(request.StartingAddress, request.State);
            return response;
        }

        public override ModbusFunction WriteSingleRegister(WriteSingleRegisterRequest request)
        {
            var response = new WriteSingleRegisterResponse(request.StartingAddress, request.Value);
            return response;
        }

        public override ModbusFunction WriteMultipleCoils(WriteMultipleCoilsRequest request)
        {
            var response = new WriteMultipleCoilsResponse(request.StartingAddress, request.Quantity);
            return response;
        }

        public override ModbusFunction WriteMultipleRegisters(WriteMultipleRegistersRequest request)
        {
            var response = new WriteMultipleRegistersResponse(request.StartingAddress, request.Quantity);
            return response;
        }

        private BitArray ReadCoilsOrInputs(ushort quantity)
        {
            var length = quantity + (8 - quantity % 8) % 8;
            var coils = new bool[length];

            Random ran = new Random();

            //for (int i = 0; i < coils.Length; i++)// from low to high
            //{
            //    if (i < quantity)
            //    {
            //        coils[i] = ran.Next() % 2 == 0; //coils[i] = true;

            //    }
            //    else
            //    {
            //        coils[i] = false;
            //    }
            //}
            coils[0] = true;
            coils[1] = false;
            coils[2] = true;
            coils[3] = true;
            coils[4] = false;
            coils[5] = true;


            var arr = new BitArray(coils);
            return arr;
        }

        private ushort[] ReadRegisters(ushort quantity)
        {
            var registers = new ushort[quantity];

            Random ran = new Random();
            //for (int i = 0; i < registers.Length; i++)
            //{
            //    registers[i] = (ushort)ran.Next(ushort.MinValue, ushort.MaxValue);
            //}

            //MyBuffer.buffer.SetFloat(0,0.0123f);

            //registers[0] = MyBuffer.buffer.GetUnsignedShort(0)  ;
            //registers[1] = MyBuffer.buffer.GetUnsignedShort(2);
            //registers[2] = 5;

            for (int i = 0; i < registers.Length; i++)
            {
                registers[i] = MyBuffer.buffer.GetUnsignedShort(i*2);
            }

            return registers;
        }
    }
}
