﻿Imports DevExpress.XtraMap
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Xml.Linq

Namespace CustomClustererSample
    Partial Public Class Form1
        Inherits Form

        Private ReadOnly Property VectorLayer() As VectorItemsLayer
            Get
                Return CType(map.Layers("VectorLayer"), VectorItemsLayer)
            End Get
        End Property
        Private ReadOnly Property DataAdapter() As ListSourceDataAdapter
            Get
                Return CType(VectorLayer.Data, ListSourceDataAdapter)
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            AddHandler VectorLayer.DataLoaded, Sub(obj, args)
                map.ZoomToFitLayerItems()
            End Sub

            DataAdapter.DataSource = LoadData()
            DataAdapter.Clusterer = New CureClusterer()
        End Sub

        Private Function LoadData() As List(Of Tree)
            Dim trees As New List(Of Tree)()
            Dim doc As XDocument = XDocument.Load("Data\treesCl.xml")
            For Each xTree As XElement In doc.Element("RowSet").Elements("Row")
                trees.Add(New Tree With { _
                    .Latitude = Convert.ToDouble(xTree.Element("lat").Value, CultureInfo.InvariantCulture), _
                    .Longitude = Convert.ToDouble(xTree.Element("lon").Value, CultureInfo.InvariantCulture), _
                    .LocationName = xTree.Element("location").Value _
                })
            Next xTree
            Return trees
        End Function
    End Class
End Namespace
