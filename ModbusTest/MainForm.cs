using Modbus.Device;
using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ModbusTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        // 读写保持寄存器
        private void button1_Click(object sender, EventArgs e)
        {
            // 读取
            Console.WriteLine("---------- 保持寄存器 ----------");
            // 创建一个TcpClient对象，连接到Modbus服务器
            using (TcpClient client = new TcpClient("127.0.0.1", 502)) 
            {
                // 创建一个Modbus主设备对象
                IModbusMaster master = ModbusIpMaster.CreateIp(client);

                // 读取保持寄存器的值（例如从地址0开始的4个寄存器）
                byte slaveAddress = 1; // 设备地址
                ushort startAddress = 0; // 起始地址
                ushort numberOfPoints = 20; // 保持寄存器的数量
                ushort[] registers = master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);

                // 打印读取到的寄存器值
                for (int i = 0; i < numberOfPoints; i++)
                {
                    Console.WriteLine($"Register {startAddress + i}: {registers[i]}");
                }
            }

            // 写入
            using (TcpClient client = new TcpClient("127.0.0.1", 502)) 
            {
                IModbusMaster master = ModbusIpMaster.CreateIp(client);
                byte slaveAddress = 1; // 设备地址
                ushort startAddress = 10; // 起始地址
                ushort value = 12; // 要写入的值
                master.WriteSingleRegister(slaveAddress, startAddress, value);

                ushort[] registers = new ushort[] { 0x01, 0x02, 0x03 }; // 要写入的值数组
                master.WriteMultipleRegisters(slaveAddress, startAddress, registers);
            }
        }

        // 读写线圈
        private void button2_Click(object sender, EventArgs e)
        {
            // 读取
            Console.WriteLine("---------- 线圈 ----------");
            using (TcpClient client = new TcpClient("127.0.0.1", 502))
            {
                IModbusMaster master = ModbusIpMaster.CreateIp(client);

                byte slaveAddress = 1; // 设备地址
                ushort startAddress = 0; // 起始地址
                ushort numberOfCoils = 10; // 要读取的线圈数量
                bool[] coils = master.ReadCoils(slaveAddress, startAddress, numberOfCoils);

                // 打印读取到的线圈状态
                for (int i = 0; i < numberOfCoils; i++)
                {
                    Console.WriteLine($"Coil {startAddress + i}: {coils[i]}");
                }
            }

            // 写入
            using (TcpClient client = new TcpClient("127.0.0.1", 502))
            {
                IModbusMaster master = ModbusIpMaster.CreateIp(client);

                byte slaveAddress = 1; // 设备地址
                ushort coilAddress = 5; // 要写入的线圈地址
                bool value = true; // 要写入的值（true表示闭合，false表示断开）
                master.WriteSingleCoil(slaveAddress, coilAddress, value);


                ushort startAddress = 10; // 起始地址
                bool[] values = new bool[] { true, false, true, false, true }; // 要写入的值数组
                master.WriteMultipleCoils(slaveAddress, startAddress, values);
            }
        }

        // 读取离散输入
        private void button3_Click(object sender, EventArgs e)
        {
            // 读取
            Console.WriteLine("---------- 离散输入 ----------");
            using (TcpClient client = new TcpClient("127.0.0.1", 502))
            {
                IModbusMaster master = ModbusIpMaster.CreateIp(client);

                byte slaveAddress = 1; // 设备地址
                ushort startAddress = 0; // 起始地址
                ushort numberOfInputs = 10; // 要读取的离散输入数量
                bool[] inputs = master.ReadInputs(slaveAddress, startAddress, numberOfInputs);
                // 打印读取到的离散输入状态
                for (int i = 0; i < numberOfInputs; i++)
                {
                    Console.WriteLine($"Discrete Input {startAddress + i}: {inputs[i]}");
                }
            }
        }

        // 读取输入寄存器
        private void button4_Click(object sender, EventArgs e)
        {
            // 读取
            Console.WriteLine("---------- 输入寄存器 ----------");
            using (TcpClient client = new TcpClient("127.0.0.1", 502))
            {
                IModbusMaster master = ModbusIpMaster.CreateIp(client);

                byte slaveAddress = 1; // 设备地址
                ushort startAddress = 0; // 起始地址
                ushort numberOfRegisters = 10; // 要读取的输入寄存器数量
                ushort[] registers = master.ReadInputRegisters(slaveAddress, startAddress, numberOfRegisters);
                // 打印读取到的输入寄存器值
                for (int i = 0; i < numberOfRegisters; i++)
                {
                    Console.WriteLine($"Input Register {startAddress + i}: {registers[i]}");
                }
            }
        }
    }
}
