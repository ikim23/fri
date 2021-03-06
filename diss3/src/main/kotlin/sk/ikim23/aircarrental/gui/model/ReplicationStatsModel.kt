package sk.ikim23.aircarrental.gui.model

import javafx.beans.property.SimpleStringProperty
import sk.ikim23.aircarrental.sim.format
import sk.ikim23.aircarrental.sim.toTime
import sk.ikim23.aircarrental.stat.SimStats

class ReplicationStatsModel {
    val time = SimpleStringProperty()
    val t1ArrivalCount = SimpleStringProperty()
    val t2ArrivalCount = SimpleStringProperty()
    val serviceDeskArrivalCount = SimpleStringProperty()
    val avgSystemTimeArrivingPassenger = SimpleStringProperty()
    val ciSystemTimeArrivingPassenger = SimpleStringProperty()
    val avgSystemTimeLeavingPassenger = SimpleStringProperty()
    val ciwSystemTimeLeavingPassenger = SimpleStringProperty()
    val avgQueueSizeT1 = SimpleStringProperty()
    val ciQueueSizeT1 = SimpleStringProperty()
    val avgQueueSizeT2 = SimpleStringProperty()
    val ciQueueSizeT2 = SimpleStringProperty()
    val avgQueueSizeService = SimpleStringProperty()
    val ciQueueSizeService = SimpleStringProperty()
    val avgQueueSizeToT3 = SimpleStringProperty()
    val ciQueueSizeToT3 = SimpleStringProperty()
    val avgWaitTimeT1 = SimpleStringProperty()
    val ciWaitTimeT1 = SimpleStringProperty()
    val avgWaitTimeT2 = SimpleStringProperty()
    val ciWaitTimeT2 = SimpleStringProperty()
    val avgWaitTimeService = SimpleStringProperty()
    val ciWaitTimeService = SimpleStringProperty()
    val avgWaitTimeToT3 = SimpleStringProperty()
    val ciWaitTimeToT3 = SimpleStringProperty()
    val drivenKm = SimpleStringProperty()
    val driveCosts = SimpleStringProperty()
    val driverCosts = SimpleStringProperty()
    val employeeCosts = SimpleStringProperty()
    val totalCosts = SimpleStringProperty()

    fun update(stats: SimStats) {
        time.set(stats.time.toTime())
        t1ArrivalCount.set(stats.t1ArrivalCount.toString())
        t2ArrivalCount.set(stats.t2ArrivalCount.toString())
        serviceDeskArrivalCount.set(stats.serviceDeskArrivalCount.toString())
        avgSystemTimeArrivingPassenger.set(stats.avgSystemTimeArrivingPassenger.toTime())
        ciSystemTimeArrivingPassenger.set("< ${stats.lowSystemTimeArrivingPassenger.toTime()} , ${stats.uppSystemTimeArrivingPassenger.toTime()} >")
        avgSystemTimeLeavingPassenger.set(stats.avgSystemTimeLeavingPassenger.toTime())
        ciwSystemTimeLeavingPassenger.set("< ${stats.lowSystemTimeLeavingPassenger.toTime()} , ${stats.uppSystemTimeLeavingPassenger.toTime()} >")
        avgQueueSizeT1.set(stats.avgQueueSizeT1.format())
        ciQueueSizeT1.set("< ${stats.lowQueueSizeT1.format()} , ${stats.uppQueueSizeT1.format()} >")
        avgQueueSizeT2.set(stats.avgQueueSizeT2.format())
        ciQueueSizeT2.set("< ${stats.lowQueueSizeT2.format()} , ${stats.uppQueueSizeT2.format()} >")
        avgQueueSizeService.set(stats.avgQueueSizeService.format())
        ciQueueSizeService.set("< ${stats.lowQueueSizeService.format()} , ${stats.uppQueueSizeService.format()} >")
        avgQueueSizeToT3.set(stats.avgQueueSizeToT3.format())
        ciQueueSizeToT3.set("< ${stats.lowQueueSizeToT3.format()} , ${stats.uppQueueSizeToT3.format()} >")
        avgWaitTimeT1.set(stats.avgWaitTimeT1.toTime())
        ciWaitTimeT1.set("< ${stats.lowWaitTimeT1.toTime()} , ${stats.uppWaitTimeT1.toTime()} >")
        avgWaitTimeT2.set(stats.avgWaitTimeT2.toTime())
        ciWaitTimeT2.set("< ${stats.lowWaitTimeT2.toTime()} , ${stats.uppWaitTimeT2.toTime()} >")
        avgWaitTimeService.set(stats.avgWaitTimeService.toTime())
        ciWaitTimeService.set("< ${stats.lowWaitTimeService.toTime()} , ${stats.uppWaitTimeService.toTime()} >")
        avgWaitTimeToT3.set(stats.avgWaitTimeToT3.toTime())
        ciWaitTimeToT3.set("< ${stats.lowWaitTimeToT3.toTime()} , ${stats.uppWaitTimeToT3.toTime()} >")
        drivenKm.set(stats.drivenKm.format())
        driveCosts.set("${stats.driveCosts.format()}€")
        driverCosts.set("${stats.driverCosts.format()}€")
        employeeCosts.set("${stats.employeeCosts.format()}€")
        totalCosts.set("${stats.totalCosts().format()}€")
    }
}